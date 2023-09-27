using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBusiness collabBusiness;
        private readonly IDistributedCache distributedCache;
        public CollabController(ICollabBusiness collabBusiness, IDistributedCache distributedCache)
        {
            this.collabBusiness = collabBusiness;
            this.distributedCache = distributedCache;
        }

        [Authorize, HttpPost]
        [Route("CreateCollab")]
        public IActionResult CreateCollab(string Email, int NoteId)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = collabBusiness.CreateCollab(Email, userId, NoteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "SuccessFully collaborated", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "CouldNot Collaborate" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize, HttpDelete]
        [Route("DeleteCollab")]

        public IActionResult DeleteCollab(int collabId)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = collabBusiness.DeleteCollab(collabId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Deleted SuccessFully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize, HttpGet]
        [Route("RetreiveAll")]

        public IActionResult RetreiveAll(int NoteId)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var res = collabBusiness.RetreiveAll(userId, NoteId);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Retreived SuccessFully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Retreival UnsuccessFull" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize, HttpGet]
        [Route("GetAllCollabByRedisCache")]

        public async Task<IActionResult> GetAllLabelsByRedisCache(int NoteId)
        {
            try
            {
                var cacheKey = $"CollabList_{User.FindFirst("UserId").Value}";

                var serializedCollabList = await distributedCache.GetStringAsync(cacheKey);
                List<CollabEntity> CollabList;
                if (serializedCollabList != null)
                {

                    CollabList = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedCollabList);
                }
                else
                {
                    int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                    CollabList = collabBusiness.RetreiveAll(userId, NoteId);
                    serializedCollabList = JsonConvert.SerializeObject(CollabList);


                    await distributedCache.SetStringAsync(cacheKey, serializedCollabList,
                        new DistributedCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                            SlidingExpiration = TimeSpan.FromMinutes(30)
                        });
                }
                return Ok(CollabList);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
