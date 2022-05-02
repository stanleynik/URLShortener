import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from "rxjs";
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { IUrl } from '../../shared/models/url';
 
export interface Response {
  status: number,
  message: string
}

@Injectable({
  providedIn: 'root'
})
export class UrlShortenerService {
 

  constructor(private http: HttpClient) { }

  addURL(URL: string): Observable<any> {
    var result: any;
    try {
  
      let body = new HttpParams();
      body = body.set('url', URL);
      const httpOptions : Object = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        }),
        responseType: 'text'
      };
  
      result = this.http.post(environment.apiUrl + "/URLShortener", JSON.stringify(URL), httpOptions);
    } catch (e) {
      alert(e);
    }
    return result;
  
  }

  getProvidedURL(ShortCode: string): Observable<any> {
    var result: any;
 
    try {

      const httpOptions: Object = {
        responseType: 'text'
      };
 
      result = this.http.get(environment.apiUrl + "/GetProvidedURL/" + ShortCode, httpOptions);
    } catch (e) {
      alert(e);
    }
    return result;
  }

  getTop20(): Observable<IUrl[]> {

    return this.http.get<IUrl[]>("https://localhost:7091/GetTop20/")
        .pipe(
        catchError(this.handleError)
      );

  }

  updateVisit(short_code: string): Observable<any> {
    var result: any;
    try {
      const httpOptions: Object = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })
      };
      var url = environment.apiUrl + "/UpdateVisits/" + short_code;
      result = this.http.post(url, httpOptions);
    } catch (e) {
      
    }
    return result;

  }


  private handleError(err: HttpErrorResponse): Observable<never> {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(errorMessage);
  }


}
