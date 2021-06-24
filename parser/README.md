# logger-restclient
## Execute directly using python
Log parser project is developed in python. Tested on python python:3.9.5.
- cd parser
- python app_parser.py log_file1.txt


## Execute using docker
Use following command to parse default log file
- docker run umairnaushad/log-parser:1.0.0
Use following command to parse specific log file
- docker run -v "C:/Users/umair.naushad/Desktop/log_file2.txt":/opt/app/log_file2.txt umairnaushad/log-parser:1.0.0

## Build using docker
- cd parser
- docker build -t umairnaushad/log-parser:1.0.0 .
- docker push umairnaushad/log-parser:1.0.0