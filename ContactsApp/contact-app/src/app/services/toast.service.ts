import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { ToastConfiguration, ToastType } from '../models/toast-configuration.model';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  private toasts: Subject<any> = new Subject<any>();

  getToasts() {
    return this.toasts.asObservable();
  }

  showToast(type: ToastType, message: string) {
    this.toasts.next(new ToastConfiguration(type, message));
  }
}
