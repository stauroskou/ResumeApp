import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Candidate } from "../Models/candidate.model";
import { Observable } from "rxjs";

@Injectable({ providedIn: 'root' })
export class CandidateService {

  private api = 'https://localhost:5000/api/candidates';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Candidate[]> {
    return this.http.get<Candidate[]>(this.api);
  }

  get(id: number): Observable<Candidate> {
    return this.http.get<Candidate>(`${this.api}/${id}`);
  }

  create(candidate: Candidate, file?: File): Observable<Candidate> {
    const formData = new FormData();
    formData.append('firstName', candidate.firstName);
    formData.append('lastName', candidate.lastName);
    formData.append('email', candidate.email);
    if (candidate.mobile) formData.append('mobile', candidate.mobile);
    if (candidate.degreeId) formData.append('degreeId', candidate.degreeId.toString());
    if (file) formData.append('cvFile', file);

    return this.http.post<Candidate>(`${this.api}/create`, formData);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }
  getById(id: number): Observable<any> {
    return this.http.get(`${this.api}/${id}`);
  }

  update(id: number, candidate: any): Observable<any> {
    return this.http.put(`${this.api}/${id}`, candidate);
  }

}