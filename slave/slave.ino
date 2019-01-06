#include <LedControl.h>
#include <Keypad.h>
#include <TimeLib.h>

const int TimeWait = 10;
const int TimeWaitAdmin = 5;

const int trigPinFirst = A5;
const int echoPinFirst = A4;
const int trigPinSecond = A2;
const int echoPinSecond = A3;

LedControl lc = LedControl(10 ,13, 12, 1);

const byte ROWS = 4; //4 lignes
const byte COLS = 4; //4 colonnes
char keys[ROWS][COLS] = {
 {'1','2','3','A'},
 {'4','5','6','B'},
 {'7','8','9','C'},
 {'*','0','#','D'}
};
byte rowPins[ROWS] = {9, 8, 7, 6}; //Lignes
byte colPins[COLS] = {5, 4, 3, 2}; //Colonnes

Keypad keypad = Keypad( makeKeymap(keys), rowPins, colPins, ROWS, COLS );

String id;

void setup() {
  unsigned int time_hold = 4;
  keypad.setHoldTime(time_hold);

  //Anti rebond
  unsigned int time_anti_rebond = 4; //4 ms
  keypad.setDebounceTime(time_anti_rebond);
  
  lc.shutdown(0,false);
  lc.setIntensity(0,2);
  lc.clearDisplay(0);
  
  pinMode(trigPinFirst, OUTPUT); // Sets the trigPin as an Output
  pinMode(echoPinFirst, INPUT); // Sets the echoPin as an Input
  pinMode(trigPinSecond, OUTPUT); // Sets the trigPin as an Output
  pinMode(echoPinSecond, INPUT); // Sets the echoPin as an Input
  
  Serial.begin(9600); // Starts the serial communication
}

void writeArduinoOn7Segment() {
 lc.setDigit(0, 3, 9, false);
} 
void loop()
{
  int rangeCpt1 = getPulseDistanceInMM(trigPinFirst, echoPinFirst);
  int rangeCpt2 = getPulseDistanceInMM(trigPinSecond, echoPinSecond);
  if (rangeCpt1 > 0 && rangeCpt1 < 230)
  {
    int timeStart = millis() / 1000;
    char key = NO_KEY;
    while (millis() / 1000 - timeStart < TimeWait && key != '#')
    {
      key = keypad.getKey();
      if (key != NO_KEY && key != '#')
      {
        if (key == '*')
          id = "";
        else
          id += key;
        timeStart = millis() / 1000;
      }
    }
    if (key != '#')
      id = "";
    Serial.print(id);
    Serial.print("\n");
  }
  else if (rangeCpt2 > 0 && rangeCpt2 < 230)
  {
    int timeStart = millis() / 1000;
    char key = NO_KEY;
    while (millis() / 1000 - timeStart < TimeWaitAdmin && key != '#')
    {
      key = keypad.getKey();
      if (key != NO_KEY && key != '#')
      {
        if (key == '*')
          id = "";
        else
          id += key;
        timeStart = millis() / 1000;
      }
    }
    if (key == '#')
    {
      Serial.print(id);
      Serial.print("\n");
    }
    Serial.print("goodbye");
    Serial.print("\n");
  }
  id = "";
}

int getPulseDistanceInMM(int trigPinOne, int echoPinOne)
{
  long duration;
  int distance;
  
  digitalWrite(trigPinOne, LOW);
  delayMicroseconds(2);
  // Sets the trigPin on HIGH state for 10 micro seconds
  digitalWrite(trigPinOne, HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPinOne, LOW);
  // Reads the echoPin, returns the sound wave travel time in microseconds
  duration = pulseIn(echoPinOne, HIGH);
  // Calculating the distance
  distance= duration*0.034/2;
  // Prints the distance on the Serial Monitor
  return (distance);
}
