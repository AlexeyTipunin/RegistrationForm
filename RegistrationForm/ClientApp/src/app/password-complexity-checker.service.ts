import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PasswordComplexityCheckerService {

  constructor() { }

  check(password: string): boolean{
    return /^(?=.*\d)(?=.*[a-zA-Z])/.test(password);
  }
}
