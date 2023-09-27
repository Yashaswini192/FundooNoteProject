using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepoLayer.Context;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBusiness labelBusiness;
        private readonly IDistributedCache distributedCache;
        public LabelController(ILabelBusiness labelBusiness, IDistributedCache distributedCache)
        {
            this.labelBusiness = labelBusiness;
            this.distributedCache = distributedCache;
        }

        [Authorize, HttpPost]
        [Route("CreateLabel")]
        public IActionResult CreateLabel(LabelModel labelmodel, int NoteId)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = labelBusiness.CreateLabel(labelmodel, userId, NoteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Created SuccessFully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label is not created" });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize, HttpGet]
        [Route("RetreiveLabel")]

        public IActionResult RetreiveLabel(int NoteId)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var res = labelBusiness.RetrieveLabel(userId, NoteId);

                if (res != null)
                {
                    return Ok(new { success = true, message = "Retreived Successfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Could not Find Label" });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize, HttpPost]
        [Route("UpdateLabel")]

        public IActionResult UpdateLabel(int labelID, string labelName)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = labelBusiness.UpdateLabel(labelID, userId, labelName);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Updated Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Could not update label" });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize, HttpDelete]
        [Route("DeleteLabel")]
        public IActionResult DeleteLabel(int NoteId)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = labelBusiness.DeleteLabel(NoteId, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Deleted Successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Could not Found label" });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize, HttpGet]
        [Route("GetAllLabelsByRedisCache")]

        public async Task<IActionResult> GetAllLabelsByRedisCache(int NoteId)
        {
            try
            {
                var cacheKey = $"LabelList_{User.FindFirst("UserId").Value}";

                var serializedLabelList = await distributedCache.GetStringAsync(cacheKey);
                List<LabelEntity> LabelList;
                if (serializedLabelList != null)
                {
                    LabelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
                }
                else
                {
                    int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                    LabelList = labelBusiness.RetrieveLabel(userId, NoteId);
                    serializedLabelList = JsonConvert.SerializeObject(LabelList);


                    await distributedCache.SetStringAsync(cacheKey, serializedLabelList,
                        new DistributedCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                            SlidingExpiration = TimeSpan.FromMinutes(30)
                        });
                }
                return Ok(LabelList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
