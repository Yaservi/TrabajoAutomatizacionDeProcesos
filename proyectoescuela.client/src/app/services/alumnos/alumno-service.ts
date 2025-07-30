import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators'; // asegúrate de importar esto
export interface Alumno {
  id: string;
  nombre: string;
  apellido: string;
  fechaNacimiento: Date;
  correo: string;
  telefono?: string;
}

@Injectable({ providedIn: 'root' })
export class AlumnoService {
  private apiUrl = '/api/alumno'; 

  constructor(private http: HttpClient) {}

  getAlumnos(pageNumber: number = 1, pageSize: number = 10) {
    return this.http.get<any>(`/api/alumno/pagination?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  registrarAlumno(alumno: any): Observable<any> {
    return this.http.post(this.apiUrl, alumno);
  }

  async obtenerAlumnoPorId(id: number): Promise<Alumno> {
    const response = await fetch(`${this.apiUrl}/${id}`);
    if (!response.ok) {
      throw new Error('Error al obtener el alumno');
    }
    return response.json();
  }

  async crearAlumno(alumno: Alumno): Promise<Alumno> {
    const response = await fetch(this.apiUrl, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(alumno),
    });
    if (!response.ok) {
      throw new Error('Error al crear el alumno');
    }
    return response.json();
  }

  actualizarAlumno(id: string, alumno: any) {
    return this.http.put(`/api/alumno/${id}`, alumno);
  }

  borrarAlumno(id: string): Observable<any> {
    return this.http.delete(`/api/alumno/${id}`);
  }

  getTodosAlumnos(): Observable<Alumno[]> {
    return this.http
      .get<any>('/api/alumno/pagination?pageNumber=1&pageSize=1000')
      .pipe(map(res => res.items || []));
  }



}
