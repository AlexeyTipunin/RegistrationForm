import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoggingService } from './logging.service';
import { Province } from '../models/province';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProvincesService {

  private url='api/Countries/{countryId}/Provinces';

  constructor(
    private http: HttpClient,
    private logger: LoggingService
  ) { }

  getProvince (countryId: number): Observable<Province[]> {
    return this.http.get<Province[]>(this.url.replace('{countryId}', countryId.toString()))
      .pipe(
        tap(_ => this.logInfo('fetched provinces')),
        catchError(this.handleError('getProvinces', []))
      );
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      this.logError(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }

  private logError(message: string) {
    this.logger.logError(`ProvincesService: ${message}`);
  }

  private logInfo(message: string) {
    this.logger.logInfo(`ProvincesService: ${message}`);
  }
}
