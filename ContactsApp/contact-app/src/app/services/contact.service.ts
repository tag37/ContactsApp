import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Contact } from '../models/Contact';

@Injectable()
export class ContactService {
  http: HttpClient;
  baseUrl: string;

  constructor(
    private router: Router,
    http: HttpClient, @Inject('BASE_URL') baseUrl: string
  ) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  getAll() {
    return this.http.get<Contact[]>(this.baseUrl + 'api/v1/Contacts');
  }

  getContactById(id: string) {
    return this.http.get<Contact>(this.baseUrl + 'api/v1/Contacts/' + id);
  }

  createContact(contact: Contact) {
    return this.http.post<Contact>(this.baseUrl + 'api/v1/Contacts', contact);
  }

  updateContact(id: string, contact: Contact) {
    return this.http.put<Contact>(this.baseUrl + 'api/v1/Contacts/' + id, contact);
  }

  deleteContact(id: string) {
    return this.http.delete(this.baseUrl + 'api/v1/Contacts/' + id);
  }
}
