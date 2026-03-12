import { Injectable } from '@angular/core';
import { Ferramenta } from './models/ferramentas';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { CreateFerramentaRequest } from './models/create-ferramenta-request';
import { UpdateFerramentaRequest } from './models/update-ferramenta-request';

@Injectable({
  providedIn: 'root',
})
export class ListaService {
  private apiUrl = 'https://localhost:5001/api/tools';
  private readonly key = 'ferramentas-cache';

  public listaFerramentas: Ferramenta[] = [];

  constructor(private http: HttpClient) {}

  listar(): Observable<Ferramenta[]> {
    return this.http.get<Ferramenta[]>(this.apiUrl).pipe(
      tap((data) => {
        this.listaFerramentas = data;
        this.salvarNoStorage();
      }),
    );
  }

  adicionar(request: CreateFerramentaRequest) {
    return this.http.post<Ferramenta>(this.apiUrl, request).pipe(
      tap((tool) => {
        this.listaFerramentas.push(tool);
        this.salvarNoStorage();
      }),
    );
  }
  atualizar(id: string, request: UpdateFerramentaRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, request).pipe(
      tap(() => {
        this.listar().subscribe();
      }),
    );
  }

  removerItem(id: string) {
    return this.http.delete(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        this.listaFerramentas = this.listaFerramentas.filter((t) => t.id !== id);
        this.salvarNoStorage();
      }),
    );
  }

  pesquisar(query: string) {
    return this.http.get<Ferramenta[]>(`${this.apiUrl}/search?query=${query}`);
  }

  carregarCache() {
    const data = localStorage.getItem(this.key);
    if (data) {
      this.listaFerramentas = JSON.parse(data);
    }
  }

  private salvarNoStorage() {
    localStorage.setItem(this.key, JSON.stringify(this.listaFerramentas));
  }

  getCache(): Ferramenta[] {
    return [...this.listaFerramentas];
  }
}
