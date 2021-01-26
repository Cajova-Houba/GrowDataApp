// připojení knihovny DHT
#include "DHT.h"
// nastavení čísla pinu s připojeným DHT senzorem
#define pinDHT 5

// odkomentování správného typu čidla
#define typDHT11 DHT11     // DHT 11

// inicializace DHT senzoru s nastaveným pinem a typem senzoru
DHT mojeDHT(pinDHT, typDHT11);

int termPin = 0;      // Analogový pin, ke kterému je termistor připojen
int termNom = 10000;  // Referenční odpor termistoru
int refTep = 25;      // Teplota pro referenční odpor
int beta = 3380;      // Beta faktor
int rezistor = 1000; // hodnota odporu v sérii


// soil hum
// nastavení čísel propojovacích pinů
#define analogPin A1
#define vccPin 4
// proměnná pro uložení času kontroly
unsigned long cas = 0;


// hodnoty posilane do RPI
float airTemp = 0;
float airHum = 0;
float soilTemp = 0;
int soilHum = 0;

// in ms
unsigned long measureInterval = 60000;

void setup() {
  Serial.begin(9600);

  setupDHT();
  setupAnalogRef();

  setupHumidityMeasure();
}

void setupDHT() {
  // zapnutí komunikace s teploměrem DHT
  mojeDHT.begin();
}

void setupAnalogRef() {
  //použití externího pinu AREF jako referenčního napětí pro A/D převodník
  analogReference(EXTERNAL);
}

void setupHumidityMeasure() {
  // nastavení datových pinů jako vstupů
  // a napájecího pinu jako výstupu
  pinMode(analogPin, INPUT);
  pinMode(vccPin, OUTPUT);
  // vypnutí napájecího napětí pro modul
  digitalWrite(vccPin, LOW);
}

void loop() {
  mereniTepVlh();
  mereniTep();
  mereniVlh();

  sendData();
  
  // pauza pro přehlednější výpis
  delay(measureInterval);
}

// Send data to RPi using the serial line.
void sendData() {
  Serial.print("Data:");
  Serial.print(airTemp);
  Serial.print(";");
  Serial.print(airHum);
  Serial.print(";");
  Serial.print(soilTemp);
  Serial.print(";");
  Serial.print(soilHum);
  Serial.println(";");
}

void mereniTepVlh() {
  // pomocí funkcí readTemperature a readHumidity načteme
  // do proměnných tep a vlh informace o teplotě a vlhkosti,
  // čtení trvá cca 250 ms
  airTemp = mojeDHT.readTemperature();
  airHum = mojeDHT.readHumidity();
  // kontrola, jestli jsou načtené hodnoty čísla pomocí funkce isnan
  if (isnan(airTemp) || isnan(airHum)) {
    // při chybném čtení vypiš hlášku
    // todo: možná zablikat diodou?
    Serial.println("Chyba při čtení z DHT senzoru!");
  } 
}

void mereniTep() {
  float napeti;
  //změření napětí na termistoru
  napeti = analogRead(termPin);
  //Serial.print("Napet: ");
  //Serial.print(napeti);
  //Serial.println("");
  

  // Konverze změřené hodnoty na odpor termistoru
  napeti = 1023 / napeti - 1;
  napeti = rezistor / napeti;

  //Výpočet teploty podle vztahu pro beta faktor
  soilTemp = napeti / termNom;         // (R/Ro)
  soilTemp = log(soilTemp);             // ln(R/Ro)
  soilTemp /= beta;                    // 1/B * ln(R/Ro)
  soilTemp += 1.0 / (refTep + 273.15); // + (1/To)
  soilTemp = 1.0 / soilTemp;            // Převrácená hodnota
  soilTemp -= 273.15;                  // Převod z Kelvinů na stupně Celsia
}

void mereniVlh() {
// pokud je rozdíl mezi aktuálním časem a posledním
  // uloženým větší než 3000 ms, proveď měření
  if (millis() - cas > 2000) {
    // zapneme napájecí napětí pro modul s krátkou pauzou
    // pro ustálení
    digitalWrite(vccPin, HIGH);
    delay(100);
    // načtení analogové a digitální hodnoty do proměnných
    soilHum = analogRead(analogPin);
    // vypnutí napájecího napětí pro modul
    digitalWrite(vccPin, LOW);
    // uložení aktuálního času do proměnné pro další kontrolu
    cas = millis();
  }
}
