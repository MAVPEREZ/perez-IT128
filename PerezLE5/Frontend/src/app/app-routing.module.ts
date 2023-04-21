import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListPostsComponent } from './components/list-posts/list-posts.component';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { PostDetailComponent } from './components/post-detail/post-detail.component';
import { RegisterPageComponent } from './components/register-page/register-page.component';

const routes: Routes = [
  { path: 'login', component: LoginPageComponent},
  { path: '', component: ListPostsComponent },
  { path: 'list-posts', component: ListPostsComponent},
  { path: 'posts/:id', component: PostDetailComponent },
  { path: 'register', component: RegisterPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
