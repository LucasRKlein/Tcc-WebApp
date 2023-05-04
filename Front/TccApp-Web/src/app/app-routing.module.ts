import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from './components/dashboard/dashboard.component';

import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { PerfilComponent } from './components/user/perfil/perfil.component';

import { AuthGuard } from './guard/auth.guard';
import { HomeComponent } from './components/home/home.component';
import { AssociadoComponent } from './components/associado/associado.component';
import { AssociadoDetalheComponent } from './components/associado/associado-detalhe/associado-detalhe.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'user', redirectTo: 'user/perfil' },
      {
        path: 'user/perfil',
        component: PerfilComponent,
      },
      //#region Associado Routes
      { path: 'associados', component: AssociadoComponent },
      { path: 'associados/detalhe/:id', component: AssociadoDetalheComponent },
      { path: 'associados/detalhe', component: AssociadoDetalheComponent },
      //#endregion

      { path: 'eventos', redirectTo: 'eventos/lista' },
      
      { path: 'dashboard', component: DashboardComponent },
    ],
  },
  {
    path: 'user',
    component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
    ],
  },
  { path: 'home', component: HomeComponent },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
