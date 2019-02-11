import { Injectable } from '@angular/core';
import { AccountWithPassword } from '../models/accountWithPassword';

@Injectable({
  providedIn: 'root'
})
export class WizardService {
  private account: AccountWithPassword;
  
  setAccount(account: AccountWithPassword){
    this.account = account;
  }

  getAccount(): AccountWithPassword{
    return this.account;
  }

  cleanUp():void{
    this.account = undefined;
  }
}
