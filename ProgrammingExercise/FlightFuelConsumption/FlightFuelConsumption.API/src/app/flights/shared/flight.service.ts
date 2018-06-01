import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import {Flight} from './flight.model'

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class FlightService {

  selectedFlight : Flight;
  flightList : Flight[];
  
  constructor(private http: HttpClient) { 
  }

  getFlights(url : string) : Observable<any>{
    return this.http.get(url, httpOptions)      
  }
  
  getFlightsNoReturn(url : string) {
    this.http.get<Flight[]>(url, httpOptions)
    .subscribe(data => {            
      this.flightList = data;
    })
  }

  postFlight(url : string, flight : Flight) : Observable<any>{
    return this.http.post(url, flight, httpOptions)
      .pipe(
        catchError(this.handleError)
      );    
  }

  putFlight(url : string, id : number, flight : Flight) : Observable<any>{
    const newurl = `${url}?id=${id}`;
    return this.http.put(newurl, flight, httpOptions)
      .pipe(
        catchError(this.handleError)
      );    
  }

  // delete contact information
  deleteFlight(url: string, id: number): Observable<any> {
    const newurl = `${url}?id=${id}`;
    return this.http.delete(newurl, httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError('Something bad happened; please try again later.');
  }

}
