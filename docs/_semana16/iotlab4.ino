#define LED D7
#define LED2 D0
#define HARD_DELAY 2000

void setup() {
    Particle.function("led", ledControl);
    pinMode(LED,OUTPUT);
    pinMode(LED2,OUTPUT);
}

void loop() {
    // Publish data
    Particle.publish("led", "on");
    digitalWrite(LED,HIGH);
    delay(HARD_DELAY);
    Particle.publish("led", "off");
    digitalWrite(LED,LOW);
    delay(HARD_DELAY);
}

int ledControl(String command) {
    if (command=="on") {
        digitalWrite(LED2,HIGH);
        return 1;
    }
    else if (command=="off") {
        digitalWrite(LED2,LOW);
        return 0;
    }
    else{
        return -1;
    }
}
