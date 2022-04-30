import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


export interface Response {
  status: number,
  message: string
}

@Injectable({
  providedIn: 'root'
})
export class UrlShortenerService {


  api_url = "https://localhost:7091/URLShortener";
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
  
      result = this.http.post(this.api_url, JSON.stringify(URL), httpOptions);
    } catch (e) {
      alert(e);
    }
    return result;
  
  }

}
