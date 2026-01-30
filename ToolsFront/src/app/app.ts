import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {


  public tagDigitada: string = '';
  public ferramenta: {nome: string; tags: string[]} = {
    nome: '',
    tags: []
  };

  public listaFerramentas: {nome:string; tags: string[]}[] =[];
  
  adicionarFerramenta(){
  if(this.ferramenta.nome.trim() === ''){
    alert('Nome inválido!');
    return;
  }

  this.listaFerramentas.push({
    nome: this.ferramenta.nome,
    tags: [...this.ferramenta.tags]
  });

  this.ferramenta.nome = '';
  this.ferramenta.tags = [];
  this.tagDigitada = '';
}
  
    deletarbotao(item: number){

      if(item !== -1){
        this.listaFerramentas.splice(item, 1);
      }else{
        alert('Item não encontrado!');
      }
    
  }

    excluirbotao(){
      if(this.listaFerramentas.length > 0){
        this.listaFerramentas = [];
      }else{
        alert('Lista já está vázia!');
      }
    }
    mostrarObjetos() {
      console.log('Lista de Ferramentas:');
      console.table(this.listaFerramentas);
    }

    processarTags(event: KeyboardEvent) {
  if (event.key === 'Enter' || event.key === ',') {
    const tag = this.tagDigitada.replace(',', '').trim();

    if (tag && !this.ferramenta.tags.includes(tag)) {
      this.ferramenta.tags.push(tag);
    }

    this.tagDigitada = '';
  }
}
}
