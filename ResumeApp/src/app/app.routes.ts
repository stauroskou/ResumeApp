import { Routes } from '@angular/router';
import { CandidateListComponent } from './features/candidates/candidate-list/candidate-list.component';
import { CandidateFormComponent } from './features/candidates/candidate-form/candidate-form.component';
import { DegreeListComponent } from './features/degrees/degree-list/degree-list.component';
import { DegreeFormComponent } from './features/degrees/degree-form/degree-form.component';

export const routes: Routes = [
    {path: '', redirectTo: 'candidates', pathMatch: 'full'},
    {path: 'candidates', component: CandidateListComponent},
    {path: 'candidates/new', component: CandidateFormComponent },
    {path: 'candidates/edit/:id', component: CandidateFormComponent },
    {path: 'degrees', component: DegreeListComponent },
    {path: 'degrees/new', component: DegreeFormComponent },
    {path: 'degrees/edit/:id', component: DegreeFormComponent },
];

