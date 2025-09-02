import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Truck } from './truck.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TruckService {
  private apiUrl = 'https://localhost:44354/api/trucks';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Truck[]> {
    return this.http.get<Truck[]>(this.apiUrl);
  }

  getById(id: string): Observable<Truck> {
    return this.http.get<Truck>(`${this.apiUrl}/${id}`);
  }

  // create(truck: Omit<Truck, 'id'>): Observable<Truck> {
  //   return this.http.post<Truck>(this.apiUrl, truck);
  // }

  create(truck: Truck): Observable<Truck> {
    return this.http.post<Truck>(this.apiUrl, truck);
  }

  update(id: string, truck: Truck): Observable<Truck> {
    return this.http.put<Truck>(`${this.apiUrl}/${id}`, truck);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
