import { Component, OnInit } from '@angular/core';
import { AccountsService } from '../services/accounts.service';
import { Account } from '../models/account';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Component({
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.css']
})
export class AccountListComponent implements OnInit {

  accountList: Account[];

  constructor(
    private accountsService: AccountsService
  ) { }

  ngOnInit() {
    this.getAccounts();
  }

  getAccounts(): void {
    this.accountsService.getAccounts()
      .subscribe(accounts => this.accountList = accounts)
  }

}
