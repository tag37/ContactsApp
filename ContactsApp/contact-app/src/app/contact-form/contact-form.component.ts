import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ContactService } from '../services/contact.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Contact } from '../models/Contact';

@Component({
  selector: 'app-contact-form',
  templateUrl: './contact-form.component.html',
  styleUrls: ['./contact-form.component.css']
})
export class ContactFormComponent implements OnInit {

  @Input() contact: Contact | undefined;
  @Input() action: string = "Insert";
  @Output() onSave: EventEmitter<boolean> = new EventEmitter();

  contactForm!: FormGroup;
  isEditMode = false;
  contactId: string | null = null;
  constructor(private fb: FormBuilder,
    private contactService: ContactService,
    private route: ActivatedRoute,
    private router: Router) {
  }

  ngOnInit(): void {
    this.initForm();
    //this.contactId = this.route.snapshot.paramMap.get('id');
    if (this.contact) {
      this.contactForm.patchValue({
        id: this.contact.id,
        firstName: this.contact.firstName,
        lastName: this.contact.lastName,
        email: this.contact.email
      });

      this.isEditMode = true;      
    }
  }

  initForm(): void {
    this.contactForm = this.fb.group({
      id: [''],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onSubmit(): void {
    if (this.contactForm!.invalid) {
      return;
    }
    const contactData = this.contactForm!.value;
    if (this.isEditMode) {
      this.updateContact(contactData);
    } else {
      this.createContact(contactData);
    }
  }

  createContact(contact: Contact): void {
    this.contactService.createContact(contact).subscribe(() => {
      //this.router.navigate(['/contacts']);
      this.onSave.emit(true);
    }, (error) => {
      this.onSave.emit(false);
    });
  }

  updateContact(contact: Contact): void {
    this.contactService.updateContact(contact.id.toString(), contact).subscribe(() => {
      //this.router.navigate(['/contacts']);
      this.onSave.emit(true);
    }, (error) => {
      this.onSave.emit(false);
    });
  }

  cancel() {
    this.onSave.emit(false);
  }
}
