export class ToastConfiguration {
  type: ToastType;
  message: string = "";
  css: string = "";  constructor(type: ToastType, message: string) {
    this.type = type;
    this.message = message;
  }
}

export enum ToastType {
  Success,
  Warning,
  Error,
  Information
}
