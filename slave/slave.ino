const int trigPinFirst = 9;
const int echoPinFirst = 10;
Servo servomotor;

long duration;
int distance;

void setup() {
  pinMode(trigPinFirst, OUTPUT); // Sets the trigPin as an Output
  pinMode(echoPinFirst, INPUT); // Sets the echoPin as an Input
  Serial.begin(9600); // Starts the serial communication
}

void loop()
{
    int distance = getPulseDistanceInMM(trigPinFirst, echoPinFirst);
    Serial.print(distance);
    Serial.print("\n");
}

int getPulseDistanceInCM(int trigPinOne, int echoPinOne){
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
