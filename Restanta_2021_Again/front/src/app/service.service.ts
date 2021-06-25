import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ServiceService {

  mainUrl = "http://localhost:8080/main";
  constructor(private httpClient: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json', })
  };
  getAll(): Observable<any> {
    return this.httpClient.get("http://localhost:8080/login");
  }

  login(username: string): Observable<any> {
    return this.httpClient.post<any>("http://localhost:8080/login?username=" + username, this.httpOptions);
  }

  addBanned(destinationId: number, user: string) {
    return this.httpClient.post(`${this.mainUrl}?destination=${destinationId}&user=${user}`, this.httpOptions);
  }

  search(destionation: string, user: string) {
    return this.httpClient.get<any>(`${this.mainUrl}?destination=${destionation}&user=${user}`);
  }

}
