# About

Simple app which receives data from Arduino and then sends them to the server using API.

## Dependencies

 - `pip3 install requests`
 - `pip3 install PyJWT`
 - `pip3 install pyserial`

## Getting data from arduino

The Arduino is connected with RPI using USB cable. [Raspberry Pi Arduino Serial Communication](https://roboticsbackend.com/raspberry-pi-arduino-serial-communication/) article destibes how exactly to do it.

 - `ls /dev/tty*` to detect Arduino (use this port in `config.ini`)
 - `sudo adduser pi dialout` add **pi** user to the **dialout** group to avoid permission issues
 - `python3 -m pip install pyserial` to install **pyserial** package

 The `pyserial` library can be used as shown in `arduinoService.py`.

 Arduino is sending the data in folowing format:

 ```
 Data:airTemp;airHum;soilTemp;soilHum;\n
 ```


## Sending data to server

Scripts are placed in the `/opt/grower` folder. Configuration is done via `config.ini`. The script can be run by `python3 main.py` wihtnout any arguments.

To run it as a service, simply copy the `grower.service` and reload the daemon:

```bash
sudo cp grower.service /lib/systemd/system/grower.service
sudo systemctl daemon-reload
sudo systemctl enable grower.service
sudo systemctl start grower.service
```

