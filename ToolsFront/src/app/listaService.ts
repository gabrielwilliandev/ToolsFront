import { Injectable } from '@angular/core';
import { Ferramenta } from './features/lista-tela-ferramentas/ferramenta';

@Injectable({
  providedIn: 'root',
})
export class ListaService {

  
  private readonly key = 'ferramentas';

  public listaFerramentas: Ferramenta[] = [];

  getLista(): Ferramenta[]{
    return [...this.listaFerramentas];
  }

  carregar(){
    this.listaFerramentas = JSON.parse(localStorage.getItem(this.key) || '[]');
  }

  
  adicionar(f: Ferramenta){
    
    this.listaFerramentas.push(f);
    this.salvarNoStorage();
    
  }
  atualizaTags(index: number, tags: string[]){
    if(!this.listaFerramentas[index]) return;

    this.listaFerramentas[index] = {
      ...this.listaFerramentas[index],
      tags: [...tags]
    };
    this.salvarNoStorage();
  }
  
  removerItem(index: number){
    this.listaFerramentas.splice(index, 1);
    this.salvarNoStorage();
  }
  
  limparLista(){
   this.listaFerramentas = []
   localStorage.removeItem(this.key);
  }
  
  salvarNoStorage(){
    localStorage.setItem(this.key, JSON.stringify(this.listaFerramentas));
  }
}

