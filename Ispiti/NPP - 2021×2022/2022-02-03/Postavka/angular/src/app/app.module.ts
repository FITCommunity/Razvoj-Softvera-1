import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { StudentiComponent } from './studenti/studenti.component';
import { RouterModule } from '@angular/router';
import { EditStudentComponent } from './studenti/edit-student/edit-student.component';
import { LoginComponent } from './login/login.component';
import { RegistracijaComponent } from './registracija/registracija.component';
import { HomeComponent } from './home/home.component';
import { AutorizacijaLoginProvjera } from './_guards/autorizacija-login-provjera.service';
import { NotFoundComponent } from './not-found/not-found.component';
import { StudentMaticnaknjigaComponent } from './student-maticnaknjiga/student-maticnaknjiga.component';
import { ChartsModule } from 'ng2-charts';
@NgModule({
  declarations: [
    AppComponent,
    StudentiComponent,
    EditStudentComponent,
    LoginComponent,
    RegistracijaComponent,
    HomeComponent,
    NotFoundComponent,
    StudentMaticnaknjigaComponent,
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      {
        path: 'studenti',
        component: StudentiComponent,
        canActivate: [AutorizacijaLoginProvjera],
      },
      {
        path: 'edit-student/:id',
        component: EditStudentComponent,
        canActivate: [AutorizacijaLoginProvjera],
      },
      { path: 'login', component: LoginComponent },
      { path: 'registracija', component: RegistracijaComponent },
      {
        path: 'student-maticnaknjiga/:id',
        component: StudentMaticnaknjigaComponent,
        canActivate: [AutorizacijaLoginProvjera],
      },
      { path: 'home', component: HomeComponent },
      {
        path: '**',
        component: NotFoundComponent,
        canActivate: [AutorizacijaLoginProvjera],
      },
    ]),
    FormsModule,
    HttpClientModule,
    ChartsModule,
  ],
  providers: [AutorizacijaLoginProvjera],
  bootstrap: [AppComponent],
})
export class AppModule {}
