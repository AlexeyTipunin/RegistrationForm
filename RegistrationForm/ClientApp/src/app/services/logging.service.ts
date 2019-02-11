import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoggingService {

  constructor() { }

  logInfo(message: string){
    console.log(message);
  }

  logWarning(message: string){
    console.warn(message);
  }

  logError(message: string){
    console.error(message);
  }
}
