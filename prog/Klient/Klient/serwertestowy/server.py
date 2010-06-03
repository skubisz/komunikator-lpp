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
        print data
                        
        toSendData = ""
        
        response = "LOGIN_FAIL"
        
        if response == "LOGIN_OK":
            toSendData = "<response><type>login</type><params><param name=\"result\" value=\"success\" /></params></response>"                        
        elif response == "LOGIN_FAIL":
            toSendData = "<response><type>login</type><params><param name=\"result\" value=\"fail\" /></params></response>"        
                                                
        self.request.send(toSendData)                

server = SocketServer.TCPServer(("localhost", PORT), RequestHandler)
server.serve_forever()    