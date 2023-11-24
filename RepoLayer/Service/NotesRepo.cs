using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RepoLayer.Service
{
    public class NotesRepo : INotesRepo
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        private readonly FileService fileService;
        private readonly Cloudinary cloudinary;

        public NotesRepo(FundooContext fundooContext, IConfiguration configuration,FileService fileService,Cloudinary cloudinary)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
            this.fileService = fileService;
            this.cloudinary = cloudinary;
        }

        //Note Creation
        public Notes CreateNote(CreateNoteModel createNote, int UserId)
        {
            try
            {
                Notes notes = new Notes();

                notes.Title = createNote.Title;
                notes.Description = createNote.Description;
                notes.Remainder = createNote.Remainder;
                notes.BgColor = createNote.BgColor;
                notes.Image = createNote.Image;
                notes.Archive = createNote.Archive;
                notes.PinNote = createNote.PinNote;
                notes.Trash = createNote.Trash;
                notes.UserId = UserId;

                fundooContext.NoteTable.Add(notes);
                fundooContext.SaveChanges();

                if (notes != null)
                {
                    return notes;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Retreiving Note 
        public Notes RetreiveNote(int NoteId,int userId)
        {
            try
            {
                var result = fundooContext.NoteTable.FirstOrDefault(x => x.NoteId == NoteId && x.UserId == userId);

                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Updating Note
        public Notes UpdateNote(CreateNoteModel createNote, int NoteId, int userId)
        {

            try
            {

                var result = fundooContext.NoteTable.FirstOrDefault(x => x.NoteId == NoteId);
                if (result != null)
                {
                    result.Title = createNote.Title;
                    result.Description = createNote.Description;
                    result.Remainder = createNote.Remainder;
                    result.BgColor = createNote.BgColor;
                    result.Image = createNote.Image;
                    result.Archive = createNote.Archive;
                    result.PinNote = createNote.PinNote;
                    result.Trash = createNote.Trash;
                    result.UserId = userId;

                    fundooContext.SaveChanges();
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Deleting Note
        public bool DeleteNote(int NoteId,int userId)
        {
            try
            {
                var deleteNote = fundooContext.NoteTable.FirstOrDefault(x => x.NoteId == NoteId && x.UserId == userId);

                if (deleteNote != null)
                {
                    fundooContext.NoteTable.Remove(deleteNote);
                    fundooContext.SaveChanges();
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }             

        //Uploading Image on Cloudinary 
        public async Task<Tuple<int, string>> Image(int NoteId, IFormFile imageFile, int userId)
        {
            try
            {
                var result = fundooContext.NoteTable.FirstOrDefault( x=>x.UserId == userId && x.NoteId == NoteId);
                if (result != null)
                {

                    var data = await fileService.SaveImage(imageFile);
                    if (data.Item1 == 0)
                    {
                        return new Tuple<int, string>(0, data.Item2);
                    }

                    var UploadImage = new ImageUploadParams
                    {
                        File = new CloudinaryDotNet.FileDescription(imageFile.FileName, imageFile.OpenReadStream())
                    };

                    ImageUploadResult uploadResult = await cloudinary.UploadAsync(UploadImage);
                    string imageUrl = uploadResult.SecureUrl.AbsoluteUri;
                    result.Image = imageUrl;

                    fundooContext.NoteTable.Update(result);
                    fundooContext.SaveChanges();

                    return new Tuple<int, string>(1, "Image Uploaded Successfully");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }


        //ISTrash
        public Notes IsTrash(int NoteId,int userId)
        {
            try
            {
                var note = fundooContext.NoteTable.FirstOrDefault(x => x.NoteId == NoteId && x.UserId == userId);
                if (note != null)
                {

                    if (note.Trash == false)
                    {
                        note.Trash = true;
                        fundooContext.SaveChanges();
                        return note;
                    }
                    else
                    {
                        note.Trash = false;
                        fundooContext.SaveChanges();
                        return note;
                    }
                    
                }
                else { return null; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Notes> GetALLNotes(int userId)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(x => x.UserId == userId).ToList();
                if(result.Count != 0)
                {
                    return result;
                }
                else
                {
                    return null;
                }

            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsArchive(int NoteId,int userId)
        {
            try
            {
                var note = fundooContext.NoteTable.FirstOrDefault(x => x.NoteId == NoteId && x.UserId == userId);
                if (note != null)
                {
                    if (note.Archive == true)
                    {
                        note.Archive = false;
                        fundooContext.SaveChanges();
                        //return true;
                    }
                    else
                    {
                        note.Archive = true;
                        fundooContext.SaveChanges();
                        //return false;
                    }
                    //fundooContext.SaveChanges();
                }
                return true;
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public bool IsPin(int NoteId, int userId)
        {
            try
            {
                var note = fundooContext.NoteTable.FirstOrDefault(x => x.NoteId == NoteId && x.UserId == userId);
                if(note.PinNote == false)
                {
                    note.PinNote = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    note.PinNote = false;
                    fundooContext.SaveChanges();
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Notes Color(int NoteId, string Color)
        {
            try
            {
                var result = fundooContext.NoteTable.FirstOrDefault(x => x.NoteId == NoteId);
                if (result != null)
                {
                    result.BgColor = Color;
                    fundooContext.SaveChanges();
                    return result;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Your tasks is to develop an API that enables users to find notes based on keywords or phrases.
        //The API should accept a search query parameter and return the search results
        public List<Notes> SearchQuery(string keyword)
        {
            try
            {
                var result = fundooContext.NoteTable.Where(x => x.Title.Contains(keyword) || x.Description.Contains(keyword)).ToList();
                if(result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}

