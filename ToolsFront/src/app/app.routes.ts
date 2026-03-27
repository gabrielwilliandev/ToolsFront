import { Routes } from '@angular/router';
import { ListaTelaFerramentas } from './features/lista-tela-ferramentas/lista-tela-ferramentas';
import { Contato } from './features/contato/contato';
import { Sobre } from './features/sobre/sobre';
import { Inicio } from './features/inicio/inicio/inicio';
import { Login } from './features/login/login';
import { Layout } from '../layout/layout';
import { Register } from './features/register/register';
import { Minhalista } from './features/minhalista/minhalista';

export const routes: Routes = [
    {
        path: '',
        component: Login
    },
    {
      path: 'register',
      component: Register
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
        path: 'minhalista',
        component: Minhalista
      },
      {
        path: 'listaferramentas',
        component: ListaTelaFerramentas
      },
      {
        path: 'listaferramentas/:id',
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
