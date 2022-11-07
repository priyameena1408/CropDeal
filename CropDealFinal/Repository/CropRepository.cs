using CaseStudy.Dtos.CropDto;
using CaseStudy.Models;
using CaseStudy.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace CaseStudy.Repository
{
    public class CropRepository : ICropRepository
    {
        DatabaseContext _context;
        ExceptionRepository _exception;
        public CropRepository(DatabaseContext context,ExceptionRepository exception)
        {
            _context = context;
            _exception = exception;
        }

        private enum CropId
        {
            Fruit = 1,
            Vegetable = 2,
            Grain = 3
        }
        #region CreateCrop
        /// <summary>
        /// Method to add a new Crop
        /// </summary>
        /// <param name="crop"></param>
        /// <returns></returns>
        public async Task<ActionResult<CropDetail>> AddCropAsync(AddCropDto crop)
        {
            try
            {
                CropDetail cropDetail = new CropDetail();
                cropDetail.CropName = crop.CropName;
                cropDetail.QtyAvailable = crop.CropQtyAvailable;
                cropDetail.Location = crop.CropLocation;
                cropDetail.ExpectedPrice = crop.CropExpectedPrice;
                cropDetail.CropImage="image.png";
                cropDetail.FarmerId = crop.fid;

                cropDetail.CropTypeId = (int)Enum.Parse(typeof(CropId), crop.CropType);

                _context.CropDetails.Add(cropDetail);
                await _context.SaveChangesAsync();

                var subs = _context.Subscriptions.Where(p => p.CropTypeId == cropDetail.CropTypeId).ToList();

                //HERE GOES CODE FOR NOTIFICATION
                //foreach (Subscription sub in subs)
                //{
                // var email = _context.Users.SingleOrDefault(p => p.UserId == sub.DealerId).Email;
                //HERE GOES THE LOGIC FOR SENDING THE MAIL
                //WHICH IS WRITTEN BELOW OUTSIDE THE LOOP
                //DUE TO APP PASSWORD, WHICH IS NOT THERE
                //SendNoti(cropDetail, email);
                //}
                SendNoti(cropDetail, "priyameena1408@gmail.com");

                return cropDetail;

            }
            catch (Exception e)
            {
               await _exception.AddException(e, "AddCrop Method in CropRepo");
            }
            return null;
        }
        #endregion

        #region GetAllCrops
        /// <summary>
        /// Get list of all crops
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CropDetail>> GetAllCropAsync()
        {
            try
            {
                var cropList = await _context.CropDetails.ToListAsync();
                if (cropList.Count > 0)
                {
                    return cropList;
                }
            }
            catch(Exception e)
            {
                await _exception.AddException(e, "GetAllCrop Method in CropRepo");
            }
            return null;
        }
        #endregion

        #region GetCropById
        /// <summary>
        /// Get single crop based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<CropDetail>> GetCropByIdAsync(int id)
        {
            try
            {
                var crop = await _context.CropDetails.FirstOrDefaultAsync(x => x.CropId == id);
                if (crop != null)
                {
                    return crop;
                }
                return null;
            }
            catch(Exception e)
            {
                await _exception.AddException(e, "GetCropById method in CropRepo");
                return null;
            }
          
        }
        #endregion

        #region EditCrop
        /// <summary>
        /// Method to edit the crop details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="crop"></param>
        /// <returns></returns>
        public async Task<ActionResult<CropDetail>> EditCropAsync(int id, UpdateCropDto crop)
        {
            try
            {
                var isExistCrop = await _context.CropDetails.FirstOrDefaultAsync(x => x.CropId == id && x.FarmerId == crop.FarmerId);

                if (isExistCrop != null)
                {
                    isExistCrop.CropName = crop.CropName;
                    isExistCrop.ExpectedPrice = crop.CropExpectedPrice;
                    isExistCrop.Location = crop.CropLocation;
                    isExistCrop.QtyAvailable = crop.CropQtyAvailable;

                    isExistCrop.CropTypeId = (int)Enum.Parse(typeof(CropId), crop.CropType);
                    _context.CropDetails.Update(isExistCrop);
                    await _context.SaveChangesAsync();
                    return isExistCrop;
                }
                return null;
            }
            catch(Exception e)
            {
                await _exception.AddException(e, "EditCrop Method in CropRepo");
                return null;
            }
            
        }
        #endregion

        #region GetCropById
        /// <summary>
        /// Get Crop By Id with more details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<ViewCropDto>> ViewCropByIdAsync(int id)
        {
            try
            {

                var crop = await _context.CropDetails.Include("CropType").Include("User").FirstOrDefaultAsync(x => x.CropId == id);
                if (crop != null)
                {
                    ViewCropDto viewCropDto = new ViewCropDto();
                    viewCropDto.CropType = crop.CropType.TypeName;
                    viewCropDto.CropName = crop.CropName;
                    viewCropDto.CropLocation = crop.Location;
                    viewCropDto.CropImg = crop.CropImage;
                    viewCropDto.CropQtyAvailable = crop.QtyAvailable;
                    viewCropDto.CropExpectedPrice = crop.ExpectedPrice;
                    viewCropDto.FarmerName = crop.User.Name;
                    viewCropDto.FarmerPhone = crop.User.Phone;
                    viewCropDto.FarmerEmail = crop.User.Email;
                    viewCropDto.FarmerId = crop.User.UserId;

                    var rat = _context.Ratings.FirstOrDefault(p => p.UserId == crop.User.UserId);
                    if(rat == null)
                    {
                        viewCropDto.FarmerRating = "No Rating";
                    }
                    else
                    {
                        viewCropDto.FarmerRating = _context.Ratings
                        .Where(p => p.UserId == crop.User.UserId)
                        .Average(p => p.TotalRating).ToString();
                    }

                    return viewCropDto;
                }
                return null;
            }
            catch (Exception e)
            {
                await _exception.AddException(e, "ViewCropById Method in CropRepo");
                return null;
            }
            
        }
        #endregion

        public async Task CropImage(string path,int cid)
        {
            var crop = await _context.CropDetails.SingleOrDefaultAsync(p => p.CropId == cid);
            crop.CropImage = path;
            _context.CropDetails.Update(crop);
            await _context.SaveChangesAsync();
        }


        private async void SendNoti(CropDetail crop,string email)
        {
            try
            {
                
                using (MailMessage message = new MailMessage("priyameena1408@gmail.com", "priyameena0814@gmail.com"))
                {
                    message.Body = "A new Crop Added\n" +
                        "------------------------------------------------------------------------------------\n" +
                        $"Crop Name: {crop.CropName}\n" +
                        $"Crop Type: " + (crop.CropTypeId == 1 ? "Fruit\n" : crop.CropTypeId == 2 ? "Vegatable\n" : "Grain\n") +
                        $"Crop Qty: {crop.QtyAvailable}\n" +
                        $"Crop ExpectedPrice: {crop.ExpectedPrice}\n" +
                        $"\n Link For the new Crop Added:   " +
                        "http://localhost:4200/crop-detail/" + crop.CropId +
                        "\n------------------------------------------------------------------------------------";

                    message.Subject = "NEW CROP ADDED";
                    message.IsBodyHtml = false;

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential cred = new NetworkCredential("priyameena1408@gmail.com", "kqwwejqhpnncppuo");
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = cred;
                        smtp.Port = 587;
                        smtp.Send(message);
                    }
                }
            }
            catch (Exception e)
            {
                await _exception.AddException(e, "Send Noti in CropRepo");
            }
        }

    }
}
