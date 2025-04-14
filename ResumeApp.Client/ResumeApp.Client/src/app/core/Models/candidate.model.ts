export interface Candidate {
    id?: number;
    firstName: string;
    lastName: string;
    email: string;
    mobile?: string;
    degreeId?: number;
    creationTime?: string;
    cv?: File;
  }