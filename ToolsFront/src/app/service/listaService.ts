import { Injectable } from '@angular/core';
import { Ferramenta } from '../models/ferramentas';
import { HttpClient } from '@angular/common/http';
import { map, tap } from 'rxjs/operators';
import { Observable, BehaviorSubject } from 'rxjs';
import { CreateFerramentaRequest } from '../models/create-ferramenta-request';
import { UpdateFerramentaRequest } from '../models/update-ferramenta-request';
import { ApiResponse } from '../models/api-response';

@Injectable({ providedIn: 'root' })
export class ListaService {
  private apiUrl = 'https://localhost:7130/api/tools';
  private readonly key = 'ferramentas-cache';

  // estado reativo
  private _ferramentas$ = new BehaviorSubject<Ferramenta[]>([]);
  public ferramentas$ = this._ferramentas$.asObservable();

  constructor(private http: HttpClient) {
    this.carregarCache(); // inicializa do storage
  }

  listar(): Observable<Ferramenta[]> {
    return this.http.get<ApiResponse<Ferramenta[]>>(this.apiUrl, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('token') || ''}`
      }
    }).pipe(
      map(res => res.data),
      tap(data => {
        this._ferramentas$.next(data);
        this.salvarNoStorage(data);
      })
    );
  }

  adicionar(request: CreateFerramentaRequest): Observable<Ferramenta> {
    return this.http.post<ApiResponse<Ferramenta>>(this.apiUrl, request).pipe(
      map(res => res.data),
      tap(tool => {
        const atual = this._ferramentas$.value;
        const novo = [...atual, tool];
        this._ferramentas$.next(novo);
        this.salvarNoStorage(novo);
      })
    );
  }

  atualizar(id: string, request: UpdateFerramentaRequest) {
    return this.http.put<ApiResponse<Ferramenta> | void>(`${this.apiUrl}/${id}`, request).pipe(
      tap(() => {
        // opcional: recarrega do backend para garantir estado consistente
        this.listar().subscribe(); // ou melhor: atualizar localmente
      })
    );
  }

  removerItem(id: string) {
    return this.http.delete<ApiResponse<void>>(`${this.apiUrl}/${id}`).pipe(
      tap(() => {
        const novo = this._ferramentas$.value.filter(t => t.id !== id);
        this._ferramentas$.next(novo);
        this.salvarNoStorage(novo);
      })
    );
  }

  carregarCache() {
    const data = localStorage.getItem(this.key);
    if (data) {
      try {
        const parsed = JSON.parse(data) as Ferramenta[];
        this._ferramentas$.next(parsed);
      } catch {
        this._ferramentas$.next([]);
      }
    }
  }

  private salvarNoStorage(data: Ferramenta[]) {
    localStorage.setItem(this.key, JSON.stringify(data));
  }

  getCache(): Ferramenta[] {
    return [...this._ferramentas$.value];
  }
}