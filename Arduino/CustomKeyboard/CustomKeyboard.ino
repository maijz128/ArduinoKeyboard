
#include "Keyboard.h"
#include "KeyCode.h"

#define KEYCODE unsigned char
const byte ROWS = 4; //four rows
const byte COLS = 4; //four columns

KEYCODE hexaKeys[ROWS][COLS] = {
  {KEY_1, KEY_2, KEY_3, KEY_A},
  {KEY_4, KEY_5, KEY_6, KEY_B},
  {KEY_7, KEY_8, KEY_9, KEY_C},
  {KEY_Q, KEY_0, KEY_T, KEY_D}
};
//定义Arduino IO口
byte rowPins[ROWS] = {9, 8, 7, 6};  //连接到行数字小键盘的管脚
byte colPins[COLS] = {5, 4, 3, 2};  //连接到列数字小键盘的管脚

//initialize an instance of class NewKeypad
Keyboard customKeyboard = Keyboard( makeKeymap(hexaKeys), rowPins, colPins, ROWS, COLS); 

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
}

void loop() {
//  Serial.println("hello");
//  delay(100);
  KEYCODE customKey = customKeyboard.getKey();
  
  if (customKey){
//    Serial.println(customKey);
    Serial.write(customKey);
  }
}
