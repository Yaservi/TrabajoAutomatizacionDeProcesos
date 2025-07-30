import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators'; // igual, asegúrate de importar esto

export interface Materia {
  id: string;
  nombreMateria: string;
  descripcion: string;
  maestroId: string;
  maestro?: any;
  alumno?: any[];
  calificacion?: any[];
  asistencias?: any[];
}

@Injectable({
  providedIn: 'root'
})

export class MateriaService {
  private apiUrl = '/api/materia';

  constructor(private http: HttpClient) { }

  obtenerMaterias(pageNumber: number = 1, pageSize: number = 10): Observable<any> {
    return this.http.get<any>(
      `${this.apiUrl}/pagination?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
  }

  obtenerMateriasPorId(id: string): Observable<Materia> {
    return this.http.get<Materia>(`${this.apiUrl}/${id}`);
  }

  crearMateria(materia: Materia): Observable<Materia> {
    return this.http.post<Materia>(this.apiUrl, materia);
  }

  actualizarMateria(id: string, materia: Partial<Materia>): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, materia);
  }

  borrarMateria(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  obtenerTodasMaterias(): Observable<Materia[]> {
    return this.http
      .get<any>('/api/materia/pagination?pageNumber=1&pageSize=1000')
      .pipe(map(res => res.items || []));
  }



}
