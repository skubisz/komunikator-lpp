# -*- coding: utf-8 -*-

import urllib2
import sys
import os         
import SocketServer

PORT = 7777

class RequestHandler(SocketServer.BaseRequestHandler):

    wwwDirectory = ""

    @classmethod
    def setWwwDirectory(self, directory):
        RequestHandler.wwwDirectory = directory        

    def canDownloadFile(self, file):
        if file.find("/../") >= 0 or file.find("/..\\") >= 0 \
            or file.find("\\..\\") >= 0 or file.find("\\../") >= 0:
            return False
             
        return True    

    def handle(self):
        data = self.request.recv(1024).strip()
        print "OTRZYMANY KOMUNIKAT: " + data
                        
        toSendData = ""
        
        #response = "CREATE_ACCOUNT_SUCCESS"
                
        
        while True:
            response = sys.stdin.readline().strip()                                     
                   
            if response == "LOGIN_OK":
                toSendData = "<response><type>login</type><params><param name=\"result\" value=\"success\" /></params></response>"                        
            elif response == "LOGIN_FAIL":
                toSendData = "<response><type>login</type><params><param name=\"result\" value=\"fail\" /></params></response>"        
            elif response == "CREATE_ACCOUNT_FAIL":
                toSendData = "<response><type>createAccount</type><params><param name=\"result\" value=\"fail\" /></params></response>"
            elif response == "CREATE_ACCOUNT_EXISTS":
                toSendData = "<response><type>createAccount</type><params><param name=\"result\" value=\"usernameExists\" /></params></response>"
            elif response == "CREATE_ACCOUNT_SUCCESS":
                toSendData = "<response><type>createAccount</type><params><param name=\"result\" value=\"success\" /></params></response>"
            elif response == "MESSAGES_1":
                toSendData = "<response><type>getMessages</type><params><param name=\"messages\" value=\"3\" /><param name=\"1\" value=\"test 1\" extra=\"przemek\" /><param name=\"2\" value=\"test 2\" extra=\"tomek\" /><param name=\"3\" value=\"test 3\" extra=\"przemek\" /></params></response>"
            else:
                toSendData = ""
                
            if toSendData == "":
                print "Nieprawidłowe polecenie"
            else:                                                                          
                self.request.send(toSendData) 
                break               

server = SocketServer.TCPServer(("localhost", PORT), RequestHandler)
server.serve_forever()    