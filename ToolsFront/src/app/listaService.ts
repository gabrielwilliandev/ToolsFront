import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ListaService {

  
  private readonly key = 'ferramentas';

  public listaFerramentas: Ferramenta[] = [];

  getLista(): Ferramenta[]{
    return this.listaFerramentas;
  }

  carregar(){
    this.listaFerramentas = JSON.parse(localStorage.getItem(this.key) || '[]');
  }

  
  adicionar(f: Ferramenta){
    
    this.listaFerramentas.push(f);
    this.salvarNoStorage();
    
  }
  
  removerItem(index: number){
    this.listaFerramentas.splice(index, 1);
    this.salvarNoStorage();
  }
  
  limparLista(){
   this.listaFerramentas = []
   localStorage.clear();
  }
  
  salvarNoStorage(){
    localStorage.setItem(this.key, JSON.stringify(this.listaFerramentas));
  }
}

export interface Ferramenta {
  nome?: string;
  descricao?: string;
  tags: string[];
}
