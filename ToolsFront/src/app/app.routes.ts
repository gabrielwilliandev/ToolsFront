import { Routes } from '@angular/router';
import { ListaTelaFerramentas } from './features/lista-tela-ferramentas/lista-tela-ferramentas';
import { Contato } from './features/contato/contato';
import { Sobre } from './features/sobre/sobre';

export const routes: Routes = [
    {
        path: '',
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
];
