import { Routes } from '@angular/router';
import { ListaTelaFerramentas } from './features/lista-tela-ferramentas/lista-tela-ferramentas';
import { Contato } from './features/contato/contato';
import { Sobre } from './features/sobre/sobre';
import { Inicio } from './features/inicio/inicio/inicio';
import { Login } from './features/login/login';
import { Layout } from '../layout/layout';

export const routes: Routes = [
    {
        path: '',
        component: Login
    },
    {
    path: '',
    component: Layout,
    children: [
      {
        path: 'inicio',
        component: Inicio
      },
      {
        path: 'listaferramentas',
        component: ListaTelaFerramentas
      },
      {
        path: 'contato',
        component: Contato
      },
      {
        path: 'sobre',
        component: Sobre
      }
    ]
  }

];
