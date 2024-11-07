import { Component, OnInit } from '@angular/core';
import { ContactService } from '../services/contact.service';
import { Contact } from '../models/Contact';
import { ToastService } from '../services/toast.service';
import { ToastType } from '../models/toast-configuration.model';

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css']
})
export class ContactListComponent implements OnInit {
  contacts: Contact[] = [];
  currentContact: Contact | undefined;
  action: string = "";
  constructor(private contactService: ContactService) {
  }

  ngOnInit(): void {
    this.loadContacts();
  }

  loadContacts() {
    this.contactService.getAll().subscribe(result => {
      this.contacts = result;
    }, (error) => {
    });
  }

  delete(id: number) {
    const userConfirmed = window.confirm('Are you sure you want to delete this contact?');

    if (userConfirmed) {
      this.contactService.deleteContact(id.toString()).subscribe(() => {
        this.loadContacts();
      });

    } else {
      console.log('Delete action was cancelled.');
    }
  }

  update(contact: Contact) {
    this.currentContact = contact
    this.action = "Update";
  }

  insert() {
    this.currentContact = undefined;
    this.action = "Insert";
  }

  save(saved: boolean) {
    this.action = "";
    if (saved)
      this.loadContacts();
  }
}
