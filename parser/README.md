# Parser
This program will read text log file and will parse it to fetch following information:
- To count total number of messages in a minute
- To count the occurance of string like "Mac Safari[4589]" in a minute
<br/>
The input file should be in a required template, one such line is as follows:
<br/>Aug 10 00:05:05 Mac Safari[4589]: KeychainGetIDCPStatus: status: on

It should start from date/time. After first occurence of colon(:), minute value is expected.

## 1.0 Execute directly using python
Log parser program is developed in python. Tested on python python:3.9.5.
- cd parser
- python app_parser.py log_file1.txt
![alt text](https://github.com/umairnaushad/parser-restclient/blob/main/snapshots/Parser-Python-Execution.png)


## 2.0 Using Docker
### 2.1 Execute using docker
Use following command to parse default log file
- docker run umairnaushad/log-parser:1.0.0
![alt text](https://github.com/umairnaushad/parser-restclient/blob/main/snapshots/Parser-Docker-Execution-Default.png)
Use following command to parse specific log file
- docker run -v "C:/Users/umair.naushad/Desktop/log_file2.txt":/opt/app/log_file2.txt umairnaushad/log-parser:1.0.0
![alt text](https://github.com/umairnaushad/parser-restclient/blob/main/snapshots/Parser-Docker-Execution-Volume.png)

### 2.2 Build using docker
- cd parser
- docker build -t umairnaushad/log-parser:1.0.0 .
- docker push umairnaushad/log-parser:1.0.0
