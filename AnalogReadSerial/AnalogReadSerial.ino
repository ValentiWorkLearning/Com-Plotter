/*
  AnalogReadSerial
  Reads an analog input on pin 0, prints the result to the serial monitor.
  Attach the center pin of a potentiometer to pin A0, and the outside pins to +5V and ground.

 This example code is in the public domain.
 */
// the setup routine runs once when you press reset:
void setup() {
  // initialize serial communication at 9600 bits per second:
  Serial.begin(9600);
}

// the loop routine runs over and over again forever:
void loop()
{
   
  Serial.print("PRESSURE_BEGIN");
  Serial.print(11.0f);
  Serial.print("PRESSURE_END");
  Serial.print("TEMP_BEGIN");
  Serial.print(random(10, 30));
  Serial.println("TEMP_END");
  delay(10);        // delay in between reads for stability
}
