import { Component, OnInit } from "@angular/core";
import { CandidateService } from "../../../core/Services/candidate.service";
import { Router } from "@angular/router";
import { MatTableModule } from "@angular/material/table";
import { CommonModule } from "@angular/common";
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-candidate-list',
  templateUrl: './candidate-list.component.html',
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatIconModule],
})
export class CandidateListComponent implements OnInit {
  candidates: any[] = [];
  displayedColumns = ['id', 'fullName', 'email', 'mobile', 'degree', 'cv', 'actions'];
  constructor(private candidateService: CandidateService, private router: Router) {}

  ngOnInit() {
    this.loadCandidates();
  }

  loadCandidates() {
    this.candidateService.getAll().subscribe(data => this.candidates = data);
  }

  delete(id: number) {
    this.candidateService.delete(id).subscribe(() => this.loadCandidates());
  }

  openForm() {
    this.router.navigate(['/candidates/new']);
  }

  edit(id: number) {
    this.router.navigate(['/candidates/edit', id]);
  }

  downloadCV(candidate: any) {
    const blob = new Blob([candidate.cv], { type: 'application/pdf' });

    const url = window.URL.createObjectURL(blob);
    
    const link = document.createElement('a');
    link.href = url;
    link.download = `CV.pdf`;

    document.body.appendChild(link);
    link.click();
    
    document.body.removeChild(link);
    window.URL.revokeObjectURL(url);
  }
}
