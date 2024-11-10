import { Component, OnInit } from '@angular/core';
import { ToastService } from '../../services/toast.service';
import { ToastConfiguration, ToastType } from '../../models/toast-configuration.model';

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.css']
})
export class ToastComponent implements OnInit {
  toasts: ToastConfiguration[] = [];
  constructor(private toastService: ToastService) { }

  ngOnInit(): void {
    this.toastService.getToasts().subscribe((toast: ToastConfiguration) => {
      this.setupCssToToast(toast)
      this.toasts.push(toast);
      setTimeout(() => this.removeToast(toast), 8000); // auto-hide after 5 seconds
    });
  }

  removeToast(toast: any) {
    this.toasts = this.toasts.filter(t => t !== toast);
  }

  setupCssToToast(toast: ToastConfiguration) {

    var typeCss = "";

    switch (toast.type) {
      case ToastType.Error:
        typeCss = "alert-danger";
        break;
      case ToastType.Success:
        typeCss = "alert-success";
        break;
      case ToastType.Warning:
        typeCss = "alert-warning";
        break;
      case ToastType.Information:
        typeCss = "alert-info";
        break;
    }
    if (!toast.css) {
      toast.css = typeCss;
    }

  }
}
