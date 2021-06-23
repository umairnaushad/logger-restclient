FROM python:3.9.5-alpine3.13

WORKDIR /opt/app

COPY . .

ENV FILE_NAME "log_file1.txt"

RUN pip install -r requirements.txt

ENTRYPOINT ["python", "app_parser.py"]
#ENTRYPOINT [ "python" ]

CMD [ "log_file1.txt" ]