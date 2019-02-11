import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoggingService } from './logging.service';
import { Country } from '../models/Country';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CountriesService {

  private url='https://localhost:44350/api/Countries';

  constructor(
    private http: HttpClient,
    private logger: LoggingService
  ) { }

  getCountries (): Observable<Country[]> {
    return this.http.get<Country[]>(this.url)
      .pipe(
        tap(_ => this.logInfo('fetched countries')),
        catchError(this.handleError('getCountries', []))
      );
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      this.logError(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }

  private logError(message: string) {
    this.logger.logError(`CountriesService: ${message}`);
  }

  private logInfo(message: string) {
    this.logger.logInfo(`CountriesService: ${message}`);
  }

}
