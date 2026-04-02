import { Routes } from '@angular/router';
import { ListaTelaFerramentas } from './features/lista-tela-ferramentas/lista-tela-ferramentas';
import { Contato } from './features/contato/contato';
import { Sobre } from './features/sobre/sobre';
import { Inicio } from './features/inicio/inicio/inicio';
import { Login } from './features/login/login';
import { Layout } from '../layout/layout';
import { Register } from './features/register/register';
import { Minhalista } from './features/minhalista/minhalista';
import { authGuard } from './guards/auth-guard';
import { loginGuard } from './guards/login-guard';


export const routes: Routes = [
    {
        path: '',
        component: Login,
        canActivate: [loginGuard]
    },
    {
      path: 'register',
      component: Register,
      canActivate: [loginGuard]
    },
    {
    path: '',
    component: Layout,
    canActivate: [authGuard],
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
