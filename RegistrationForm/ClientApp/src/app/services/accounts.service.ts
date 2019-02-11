import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoggingService } from './logging.service';
import { Account } from '../models/account';
import { AccountWithPassword } from '../models/accountWithPassword';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class AccountsService {

  private url = 'api/Accounts';

  constructor(
    private http: HttpClient,
    private logger: LoggingService
  ) { }

  getAccounts(): Observable<Account[]> {
    return this.http.get<Account[]>(this.url)
      .pipe(
        tap(_ => this.logError('fetched accounts')),
        catchError(this.handleError('getAccounts', []))
      );
  }

  getAccountNo404<Account>(login: string): Observable<Account> {
    const url = `${this.url}/GetByLogin/${login}`;
    return this.http.get<Account[]>(url)
      .pipe(
        map(accounts => accounts[0]),
        tap(h => {
          const outcome = h ? `fetched` : `did not find`;
          this.logError(`${outcome} account login=${login}`);
        }),
        catchError(this.handleError<Account>(`getAccountNo404 login=${localStorage}`))
      );
  }

  addAccount (account: AccountWithPassword): Observable<Account> {
    return this.http.post<Account>(this.url, account, httpOptions).pipe(
      tap((newAccount: Account) => this.logError(`added account w/ id=${newAccount.accountId}`)),
      catchError(this.handleError<Account>('addAccount'))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      this.logError(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }

  private logError(message: string) {
    this.logger.logError(`AccountsService: ${message}`);
  }
}
