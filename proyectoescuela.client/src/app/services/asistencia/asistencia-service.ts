import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Asistencia {
  id: string;
  estado: string;
  fechaAsistencia: Date;
  alumnoId: string;
  materiaId: string;
  alumno?: any;
  materia?: any;
}

@Injectable({
  providedIn: 'root'
})
export class AsistenciaService {
  private apiUrl = '/api/asistencia';

  constructor(private http: HttpClient) { }

  obtenerAsistencias(pageNumber: number = 1, pageSize: number = 10): Observable<any> {
    return this.http.get<any>(
      `${this.apiUrl}/pagination?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
  }

  obtenerAsistenciasPorId(id: string): Observable<Asistencia> {
    return this.http.get<Asistencia>(`${this.apiUrl}/${id}`);
  }

  crearAsistencia(asistencia: Asistencia): Observable<Asistencia> {
    return this.http.post<Asistencia>(this.apiUrl, asistencia);
  }

  actualizarAsistencia(id: string, asistencia: Partial<Asistencia>): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, asistencia);
  }

  borrarAsistencia(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
