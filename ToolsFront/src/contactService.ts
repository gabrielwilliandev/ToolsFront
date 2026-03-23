import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ContactRequest } from './app/models/contact-request';

@Injectable({
  providedIn: 'root',
})

export class ContactService {

  constructor(private http: HttpClient) { }

  sendContact(data: ContactRequest){
    return this.http.post('https://localhost:7130/api/email', data)
  }
}

