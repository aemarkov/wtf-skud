/*
 * 
 * Подключение считывателя RFID к Arduino
 * -----------------------------------------------------------------------------------------
 *             MFRC522      Arduino       Arduino   Arduino    Arduino          Arduino
 *             Reader/PCD   Uno/101       Mega      Nano v3    Leonardo/Micro   Pro Micro
 * Signal      Pin          Pin           Pin       Pin        Pin              Pin
 * -----------------------------------------------------------------------------------------
 * RST/Reset   RST          9             49        D9         RESET/ICSP-5     RST
 * SPI SS      SDA(SS)      10            53        D10        10               10
 * SPI MOSI    MOSI         11 / ICSP-4   51        D11        ICSP-4           16
 * SPI MISO    MISO         12 / ICSP-1   50        D12        ICSP-1           14
 * SPI SCK     SCK          13 / ICSP-3   52        D13        ICSP-3           15
 */

#include <SPI.h>
#include <MFRC522.h>

#define RST_PIN         49
#define SS_PIN          53
#define DIRECTION_PIN   A0
#define GREEN_PIN       2
#define RED_PIN         3
#define SOUND_PIN       4

MFRC522 rfid(SS_PIN, RST_PIN);
uint8_t header[]={0x37, 0x83};

// Направление движения
enum Direction
{
  NONE,
  IN,
  OUT
};

enum Access
{
  ACCESS_NONE,
  GRANTED,
  DENIED
};

void setup()
{ 
  Serial.begin(9600);
  SPI.begin();
  rfid.PCD_Init();

  pinMode(GREEN_PIN, OUTPUT);
  pinMode(RED_PIN, OUTPUT);
  pinMode(SOUND_PIN, OUTPUT);
}
 
void loop()
{
  // Ждем карту
  if ( ! rfid.PICC_IsNewCardPresent())
    return;

  // Проверяем, что UID считатн
  if ( ! rfid.PICC_ReadCardSerial())
    return;

  // Проверяем тип карты
  MFRC522::PICC_Type piccType = rfid.PICC_GetType(rfid.uid.sak);

  // Check is the PICC of Classic MIFARE type
  if (piccType != MFRC522::PICC_TYPE_MIFARE_MINI &&  
      piccType != MFRC522::PICC_TYPE_MIFARE_1K &&
      piccType != MFRC522::PICC_TYPE_MIFARE_4K)
  {    
    return;
  }

  // Проверяем направление движение
  Direction dir = get_direction();
  
  // Формируем пакет  
  Serial.write(header, sizeof(header));
  Serial.write(rfid.uid.uidByte, rfid.uid.size);
  Serial.write((uint8_t*)&dir, sizeof(dir)); 
  Serial.flush();

  // Завершаем операцию с картой
  rfid.PICC_HaltA();
  rfid.PCD_StopCrypto1();

  // Ждем ответ
  int accessRaw = get_response(); 
  if(accessRaw == -1)
  {
    // Таймаут, ответ не пришел    
    beep(200, 100);
    return;
  }
    
  
  // Если получен ответ от ПК
  Access access = (Access)accessRaw;

  if(access == GRANTED)
  {
    digitalWrite(GREEN_PIN, 1);
    beep_granted();
    delay(500);
    digitalWrite(GREEN_PIN, 0);
  }
  else if(access == DENIED)
  {
    digitalWrite(RED_PIN, 1);
    beep_denied();
    delay(500);
    digitalWrite(RED_PIN, 0);
  }
  else
  {
    beep_info();
  }   
}

int get_response()
{
  int cnt = 1000;
  while(Serial.available()==0 && cnt > 0)  
  {
    cnt--;  
    delay(1);
  }
    
  int x = Serial.read();
  while(Serial.available() > 0)
    Serial.read();
  return x;
}

// Определяет направление движения
Direction get_direction()
{
  int dirRaw = analogRead(DIRECTION_PIN);
  Direction dir = NONE;

  if(dirRaw < 100)
    dir = IN;
  else if(dirRaw > 900)
    dir = OUT;

  return dir;
}

void beep_info()
{
  beep(1000, 100);
}

void beep_granted()
{
  beep(1000, 300);
}

void beep_denied()
{
  for(int i = 0; i<3; i++)
  {
    beep(200, 100);
    delay(100);
  }
}

void beep(int freq, int duration)
{
  tone(SOUND_PIN, freq);
  delay(duration);
  noTone(SOUND_PIN);
}

