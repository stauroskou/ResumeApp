import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Degree } from "../Models/degree.model";
import { HttpClient } from "@angular/common/http";

@Injectable({ providedIn: 'root' })
export class DegreeService {
  private api = 'https://localhost:5000/api/degrees';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Degree[]> {
    return this.http.get<Degree[]>(this.api);
  }

  create(degree: Degree): Observable<Degree> {
    return this.http.post<Degree>(this.api, degree);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }

  getById(id: number): Observable<Degree> {
    return this.http.get<Degree>(`${this.api}/${id}`);
  }

  update(id: number, degree: Degree): Observable<Degree> {
    return this.http.put<Degree>(`${this.api}/${id}`, degree);
  }
}