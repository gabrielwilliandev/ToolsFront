import { Component} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Header } from './components/header/header';
import { ListaTelaFerramentas } from './features/lista-tela-ferramentas/lista-tela-ferramentas';
import { Contato } from './features/contato/contato';
import { Sobre } from './features/sobre/sobre';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, Header, ListaTelaFerramentas, Contato, Sobre],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {

  
}
